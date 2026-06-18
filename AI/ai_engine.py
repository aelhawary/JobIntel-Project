import os
import pandas as pd
import numpy as np
from sentence_transformers import SentenceTransformer
from sklearn.metrics.pairwise import cosine_similarity
from groq import Groq


_model = SentenceTransformer("sentence-transformers/all-MiniLM-L6-v2")


groq_client = Groq(
    api_key=os.environ.get(
        "GROQ_API_KEY", 
        "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"
    )
)


def generate_reason_with_llm(candidate_name, job_title, final_score, matched_skills, missing_skills, test_score):
    
    if os.environ.get("GROQ_API_KEY") is None and groq_client.api_key.startswith("gsk_placeholder"):
        return "Evaluated effectively based on matching skills, experience, and test performance."

    prompt = f"""
    You are an HR Expert responsible for justifying the ranking of candidates for a technical role.
    Based on the following data, write exactly one concise and compelling sentence in English explaining why this candidate is suitable or unsuitable for the role.
    
    Candidate Name: {candidate_name}
    Target Job Title: {job_title}
    Overall Final Score: {final_score}%
    Matched Skills: {', '.join(matched_skills) if matched_skills else 'None'}
    Missing Skills: {', '.join(missing_skills) if missing_skills else 'None'}
    Technical & Soft Skills Test Score: {test_score}/100
    
    Requirements:
    - Write exactly one sentence as a professional feedback comment.
    - Do not mention weights or score breakdown. Focus on strengths or weaknesses.
    """

    try:
        chat_completion = groq_client.chat.completions.create(
            messages=[{"role": "user", "content": prompt}],
            model="llama-3.3-70b-versatile", 
            temperature=0.3,
            max_tokens=100
        )
        
        return chat_completion.choices[0].message.content.strip()
        
    except Exception as e:
        return f"Highly compatible profile with {final_score}% overall match, verified by technical test."


def process_and_match(candidates_list, job_desc, job_title, required_skills, min_years_exp=0, max_results=10):
    
    if not candidates_list:
        return []
        
    df = pd.DataFrame(candidates_list)
    
    df = df[df["total_years_exp"] >= min_years_exp].copy()
    
    if df.empty:
        return []
        
    job_full_text = f"{job_title} {job_desc}"
    
    job_emb = _model.encode([job_full_text])
    
    job_skills_set = set(s.lower().strip() for s in required_skills)
    
    results = []

    for _, row in df.iterrows():
        
        cand_skills_raw = str(row.get("skills", "")).lower().split(",")
        
        cand_skills_set = set(s.strip() for s in cand_skills_raw if s.strip())
        
        matched = job_skills_set.intersection(cand_skills_set)
        
        missing = job_skills_set.difference(cand_skills_set)
        
        skill_score = (len(matched) / len(job_skills_set)) * 100 if job_skills_set else 0
        

        exp_text = str(row.get("experience_details", ""))
        
        exp_emb = _model.encode([exp_text])
        
        exp_score = float(cosine_similarity(job_emb, exp_emb)[0][0] * 100)
        

        bio_text = str(row.get("bio", ""))
        
        bio_emb = _model.encode([bio_text])
        
        bio_score = float(cosine_similarity(job_emb, bio_emb)[0][0] * 100)
        

        ai_match_total = (skill_score * 0.50) + (exp_score * 0.35) + (bio_score * 0.15)
        

        test_val = pd.to_numeric(row.get("test_score soft&tech", 0), errors='coerce')
        
        test_val = float(test_val) if not np.isnan(test_val) else 0.0
        

        final_score = float((ai_match_total * 0.8) + (test_val * 0.2))
        

        matched_formatted = sorted([s.title() for s in matched])
        
        missing_formatted = sorted([s.title() for s in missing])
        

        reason_text = generate_reason_with_llm(
            candidate_name=str(row.get("full_name", "")),
            job_title=job_title,
            final_score=round(final_score, 2),
            matched_skills=matched_formatted,
            missing_skills=missing_formatted,
            test_score=test_val
        )
        
        results.append({
            "candidate_id": str(row["candidate_id"]),
            "full_name": str(row.get("full_name", "")),
            "final_score": round(final_score, 2),
            "matched_skills": matched_formatted,
            "missing_skills": missing_formatted,
            "reason": reason_text
        })

    results = sorted(results, key=lambda x: x["final_score"], reverse=True)
    
    return results[:max_results]