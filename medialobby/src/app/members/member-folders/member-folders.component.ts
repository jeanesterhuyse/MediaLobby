import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs'
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';
@Component({
  selector: 'app-member-folders',
  templateUrl: './member-folders.component.html',
  styleUrls: ['./member-folders.component.css']
})
export class MemberFoldersComponent implements OnInit {
  member: Member;
  constructor(private memberService: MembersService,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember(){
    this.memberService.getMember(this.route.snapshot.paramMap.get('userEmail')).subscribe(member => {
      this.member = member;
      console.log(member);
    })
  }

  
  }


