import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs'
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';

@Component({
  selector: 'app-member-folders',
  templateUrl: './member-folders.component.html',
  styleUrls: ['./member-folders.component.css']
})
export class MemberFoldersComponent implements OnInit {
  member: Member;
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

    getImages(): NgxGalleryImage[]{
    const imageUrls =[];
    var array = this.member["$values"];
    console.log(array);
    // for (const photo of this.member.photos) {
    //   imageUrls.push({
    //     small: photo?.url,
    //     medium: photo?.url,
    //     big: photo?.url
    //   })
    // }
    return imageUrls;
  }

  loadMember(){
    this.memberService.getMember(this.route.snapshot.paramMap.get('userEmail')).subscribe(member => {
      this.member = member;
      console.log(this.member.photos);
      this.galleryImages = this.getImages();
    })
  }

  
  }


