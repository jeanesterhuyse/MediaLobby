import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { AccountService } from './account.service';


@Injectable({
  providedIn: 'root'
})
export class MembersService {

  baseUrl = environment.apiUrl;
  members: Member[]=[];


  constructor(private http: HttpClient,  private accountService: AccountService) { }

  getMembers(){
    if(this.members.length>0) return of(this.members);
    return this.http.get<Member[]>(this.baseUrl + 'users').pipe(
      map(members=> {
        this.members=members;
        return members;
      })
    )
  }

  getMember(userEmail: string) {
    const member=this.members.find(x=>x.userEmail===userEmail);
    if(member !== undefined) return of(member);
    return this.http.get<Member>(this.baseUrl + 'users/' + userEmail);
  }

updateMember(member: Member){
  return this.http.put(this.baseUrl+'users',member);
  map(()=>{
    const index=this.members.indexOf(member);
    this.members[index]=member;
  })
}

setMainPhoto(photoId: number){
return this.http.put(this.baseUrl+'users/set-main-photo/'+photoId,{})
}

UpdatePhoto(photoId: number,folders_id: number){
console.log(photoId,folders_id)
return this.http.put(this.baseUrl+'users/set-foldersId/'+photoId+'/'+folders_id,{})
}

deletePhoto(photoId: number){
  return this.http.delete(this.baseUrl+'users/delete-photo/'+photoId)
}
}
