import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { Folder } from '../_models/folder';
import { AccountService } from './account.service';


@Injectable({
  providedIn: 'root'
})
export class MembersService {

  baseUrl = environment.apiUrl;
  members: Member[]=[];
  folders: Folder[]=[];


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
  deleteFolder(folder_Id: Number){
    return this.http.delete(this.baseUrl+ 'users/delete-folder/'+folder_Id);
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

CreateFolder(create_folder_name: string){
  console.log(this.baseUrl);
  return this.http.post(this.baseUrl + 'users/create-folder/'+create_folder_name,{});
}

GetLast(){
 return this.http.get(this.baseUrl+'users/getlast');
}

updateMetaData(location: string,tags: string,date:string,capturedBy: string,photoId:Number){
  console.log(location+'/'+tags+'/'+capturedBy+'/'+this.GetLast());

return this.http.post(this.baseUrl+'users/create-metadata/'+location+'/'+tags+'/'+capturedBy+'/'+photoId,{})
}

updateFolder(folder:Folder){
  //return this.http.put(this.baseUrl+'users/update-folder/'+folderId+'/'+name,{});
  return this.http.put(this.baseUrl+'users',folder);
}

getMember(userEmail: string) {
  const member=this.members.find(x=>x.userEmail===userEmail);
  if(member !== undefined) return of(member);
  return this.http.get<Member>(this.baseUrl + 'users/' + userEmail);
}

getFolder(id:Number){
  const folder=this.folders.find(x=>x.id===id);
    return this.http.get<Folder>(this.baseUrl + 'users/get-folder/' + id);
}
}

