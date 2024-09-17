import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Skill } from '../model/skill.model';

@Injectable({
  providedIn: 'root'
})
export class SkillService {
  public backendUrl = 'https://ide-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/proxy/3001/skills'; 

  constructor(private http: HttpClient) { }

  getSkills(): Observable<Skill[]> {
    return this.http.get<Skill[]>(this.backendUrl);
  }

  addSkill(skill: Skill): Observable<Skill> {
    return this.http.post<Skill>(this.backendUrl, skill);
  }

  getSkillById(id: number): Observable<Skill> {
    const url = `${this.backendUrl}/${id}`;
    return this.http.get<Skill>(url);
  }
  updateSkill(id: number, skill: Skill): Observable<Skill> {
    const url = `${this.backendUrl}/${id}`;
    return this.http.put<Skill>(url, skill);
  }

  deleteSkill(id: number): Observable<void> {
    const url = `${this.backendUrl}/${id}`;
    return this.http.delete<void>(url);
  }
}
