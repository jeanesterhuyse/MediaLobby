import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

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
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  member: Member;
  user: User;
  user_folders = [];
  create_folder_name : string;
  strIntoObj: any[];
  baseUrl= environment.apiUrl;

  constructor(private accountService: AccountService, private membersService: MembersService, 
    private toastr: ToastrService, private http: HttpClient) { 
      this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.loadMember();
  }
  loadMember(){
      this.membersService.getMember(this.user.userEmail).subscribe(member => {
        this.member = member;
        this.user_folders = get_folders(this.member.folders);
    });
  }

  create_folder()
  {
    let myfolder: string = '[{"folderName":"'+this.create_folder_name+'"}]';
    console.log(myfolder);
    this.strIntoObj = JSON.parse(myfolder);
    console.log(this.strIntoObj);
    this.http.post(this.baseUrl + 'users/create-folder',this.strIntoObj );
  }

  updateMember(){
    this.membersService.updateMember(this.member).subscribe(()=> {
      this.toastr.success('Your profile has been updated successfully');
      this.editForm.reset(this.member);
      
    })
  
 
   
}
}
