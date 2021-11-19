import { HttpClient } from '@angular/common/http';
import { CoreEnvironment } from '@angular/compiler/src/compiler_facade_interface';
import { Component, Input, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/_models/member';
import { Photo } from 'src/app/_models/photo';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import { environment } from 'src/environments/environment';



function get_folders(folder : Object)
{
  var array = folder["$values"];
  const folders = [];
  for(let i = 0 ; i < array.length ; i++)
  {
      folders.push(array[i]);
      
  }
  return folders;
}

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
@Input() member: Member;
uploader: FileUploader;
hasBaseDropzoneOver= false;
baseUrl= environment.apiUrl;
photo_id : number ;
user: User;
user_folders = [];
images=[];
selected_folder_id : number ;


  constructor(private accountService: AccountService, private memberService: MembersService, private http: HttpClient) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user=> this.user=user);
  }

  ngOnInit(): void {
    this.images=this.getImages();
    this.user_folders = get_folders(this.member.folders);
    console.log(this.member)
    this.initializeUploader();
  }

  select_change_handler(event: any)
  {
    this.selected_folder_id = Number(event.target.value);
    console.log("Folder id from event: " + this.selected_folder_id);
  }

  setMainPhoto(photo: Photo){
  this.memberService.setMainPhoto(photo.id).subscribe(()=>{
  this.user.photoUrl=photo.url;
  this.accountService.setCurrentUser(this.user);
  this.member.photoUrl=photo.url;
  this.member.photos["$values"].forEach(p=>{
    if(p.isMain) p.isMain=false;
    if(p.id===photo.id) p.isMain=true;
  })
})
  }

  deletePhoto(photoId: number){
    this.memberService.deletePhoto(photoId).subscribe(()=>{
      this.member.photos=this.member.photos["$values"].filter(x=>x.id !== photoId);
    })
  }

  UpdatePhoto(){
    console.log(this.photo_id,this.selected_folder_id);
    this.http.put(this.baseUrl+'users/set-foldersId/'+this.photo_id+'/'+this.selected_folder_id,{})
  }

  set_photo_id(id : number)
  {
    this.photo_id = id ;
    console.log(this.photo_id);
  }
  
  fileOverBase(Any: any){
this.hasBaseDropzoneOver=Any;
  }

  getImages(){
     const images = [];
     for (const photo of this.member.photos["$values"]) {
     images.push(photo)
  }
  return images;
  }

  initializeUploader(){
this.uploader=new FileUploader({
url:this.baseUrl+'users/add-photo',
authToken: 'Bearer '+this.user.token,
isHTML5:true,
allowedFileType:['image'],
autoUpload:false,
removeAfterUpload:true,

    });
    this.uploader.onAfterAddingFile=(file)=>{
      file.withCredentials=false;
    }
    this.uploader.onSuccessItem=(item,response,status,headers)=>{
      if (response) {
        const photo=JSON.parse(response);
        this.member.photos.push(photo);
        if(photo.isMain){
          this.user.photoUrl=photo.url;
          this.member.photoUrl=photo.url;
          this.accountService.setCurrentUser(this.user);
        }
      }
    }
  }

}
