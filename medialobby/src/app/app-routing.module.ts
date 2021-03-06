import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomefrmComponent } from './homefrm/homefrm.component';
import { ListsComponent } from './lists/lists.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberFoldersComponent } from './members/member-folders/member-folders.component';
import { MembersDetailComponent } from './members/members-detail/members-detail.component';
import { MembersListComponent } from './members/members-list/members-list.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';

import { AboutComponent} from './about/about.component';

const routes: Routes = [
  {path: 'about', component:AboutComponent},
  {path: '', component: HomefrmComponent},
  {
    path: '', 
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'members', component: MembersListComponent, canActivate:[AuthGuard]},
      {path: 'members/:userEmail', component: MembersDetailComponent},
      {path: 'member/edit', component: MemberEditComponent},
      {path: 'lists', component: ListsComponent},
      {path: 'messages', component: MessagesComponent},
      {path: 'folders/:id/:userEmail', component:MemberFoldersComponent},
      

    ]
  },
  {path: '**', component: HomefrmComponent, pathMatch: 'full'},  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
