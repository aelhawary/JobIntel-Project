import uvicorn
from fastapi import FastAPI, HTTPException
from pydantic import BaseModel, Field
from typing import List, Optional
import ai_engine


app = FastAPI(
    title="AI Recruitment System v3.0",
    description="Professional Pipeline: 50% Skills, 35% Experience, 15% Bio | Final: 80% AI + 20% Test"
)


class Job(BaseModel):
    id: int
    title: str
    description: str
    minYearsOfExperience: int
    required_skills: List[str]


class Candidate(BaseModel):
    candidate_id: str
    full_name: str
    total_years_exp: int
    bio: str
    experience_details: str
    skills: str
    education: str
    test_score_soft_tech: float = Field(..., alias="test_score soft&tech")

    class Config:
        populate_by_name = True


class RecommendRequest(BaseModel):
    job: Job
    maxResults: Optional[int] = 10
    candidates: List[Candidate]


@app.post("/api/recommend")
async def recommend(request: RecommendRequest):
    
    try:
        
        candidates_data = [
            c.model_dump(by_alias=True) for c in request.candidates
        ]
        
        results = ai_engine.process_and_match(
            candidates_list=candidates_data,
            job_desc=request.job.description,
            job_title=request.job.title,
            required_skills=request.job.required_skills,
            min_years_exp=request.job.minYearsOfExperience,
            max_results=request.maxResults
        )
        
        return {
            "job": request.job,
            "maxResults": request.maxResults,
            "results": results
        }
        
    except Exception as e:
        
        print(f"Server Error: {str(e)}")
        
        raise HTTPException(status_code=500, detail="Internal Server Error")


if __name__ == "__main__":
    
    uvicorn.run(app, host="0.0.0.0", port=7860)