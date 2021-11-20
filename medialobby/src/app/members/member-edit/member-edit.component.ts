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
import { Router } from '@angular/router';

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
  update_folder_name : string;
  strIntoObj: any[];
  baseUrl= environment.apiUrl;
  selected_folder_id : number ;

  constructor(private router: Router,private accountService: AccountService, private membersService: MembersService, 
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
  select_change_handler(event: any)
  {
    this.selected_folder_id = Number(event.target.value);
  }

  create_folder()
  {
    console.log(this.create_folder_name);
    this.membersService.CreateFolder(this.create_folder_name).subscribe(response => {
      this.router.navigateByUrl('/members'),this.toastr.success('Your folder has been created');;
  })
}

  updateMember(){
    this.membersService.updateMember(this.member).subscribe(()=> {
      this.toastr.success('Your profile has been updated successfully');
      this.editForm.reset(this.member);
      
    })
   
}
updateFolder(){
  this.membersService.updateFolder(this.selected_folder_id,this.update_folder_name) 
  .subscribe(()=> {
    this.toastr.success('Your profile has been updated successfully');
    window.location.reload();
})
}

deleteFolder(){
  this.membersService.deleteFolder(this.selected_folder_id).subscribe(()=> {
    this.toastr.success('Your profile has been updated successfully');
    window.location.reload();
})
}

}