import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';

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
  selector: 'app-members-detail',
  templateUrl: './members-detail.component.html',
  styleUrls: ['./members-detail.component.css']
})
export class MembersDetailComponent implements OnInit {
  member: Member;
  email : string ;
  folders_name: string[];
  folders_id: string[];
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];

  constructor(private memberService: MembersService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadMember();

    this.galleryOptions = [
      {
      width: '500px',
      height: '500px',
      imagePercent: 100,
      thumbnailsColumns: 4,
      imageAnimation: NgxGalleryAnimation.Slide,
      preview: false
      }
    ]
    
  }




  loadMember(){
    this.memberService.getMember(this.route.snapshot.paramMap.get('userEmail')).subscribe(member => {
      this.member = member;
      this.email = member.userEmail;
      this.folders_name = get_folders(this.member.folders);
    })
  }

}
