import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs'
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions, NgxGalleryModule } from '@kolkov/ngx-gallery';

@Component({
  selector: 'app-member-folders',
  templateUrl: './member-folders.component.html',
  styleUrls: ['./member-folders.component.css']
})
export class MemberFoldersComponent implements OnInit {
  member: Member;
  folder_id : string ;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
 
  constructor(private memberService: MembersService,  private route: ActivatedRoute) { }

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

  getImages(folder_id : string): NgxGalleryImage[]{
    const imageUrls = [];
    console.log(folder_id);
    for (const photo of this.member.photos["$values"]) {
      if(photo.foldersId == folder_id)
      {
          console.log(photo)
          imageUrls.push({
          small: photo?.url,
          medium: photo?.url,
          big: photo?.url
        });
      }
    }
    return imageUrls;
  }

  loadMember(){
    this.route.paramMap.subscribe(params => {    
      this.folder_id = params.get("id"); 
    });
    this.memberService.getMember(this.route.snapshot.paramMap.get('userEmail')).subscribe(member => {
      this.member = member;
      this.galleryImages = this.getImages(this.folder_id);  
    });
    
  }
  }


