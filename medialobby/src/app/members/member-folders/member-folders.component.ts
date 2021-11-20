import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs'
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions, NgxGalleryModule } from '@kolkov/ngx-gallery';
import { AccountService } from 'src/app/_services/account.service';
import { User } from 'src/app/_models/user';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-member-folders',
  templateUrl: './member-folders.component.html',
  styleUrls: ['./member-folders.component.css']
})
export class MemberFoldersComponent implements OnInit {
  member: Member;
  folder_id : Number;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  user: User;
 
  constructor(private accountService: AccountService,private memberService: MembersService,  private route: ActivatedRoute) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
   }

  ngOnInit(): void {
    this.loadMember();

    this.galleryOptions = [
      {
      width: '600px',
      height: '600px',
      imagePercent: 100,
      thumbnailsColumns: 4,
      imageAnimation: NgxGalleryAnimation.Slide,
      preview: false
      
      }
    ]

  }

  getImages(folder_id : Number): NgxGalleryImage[]{
    const imageUrls = [];
    console.log(folder_id);
    for (const photo of this.member.photos["$values"]) {
      if(Number(photo.folderId) == Number(folder_id))
          console.log("")
        else{
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
      this.folder_id = Number(params.get("id")); 
    });
    this.memberService.getMember(this.route.snapshot.paramMap.get('userEmail')).subscribe(member => {
      this.member = member;
      this.galleryImages = this.getImages(this.folder_id); 
      
    });
    
  }


}