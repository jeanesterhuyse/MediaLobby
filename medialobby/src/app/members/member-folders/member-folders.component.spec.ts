import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MemberFoldersComponent } from './member-folders.component';

describe('MemberFoldersComponent', () => {
  let component: MemberFoldersComponent;
  let fixture: ComponentFixture<MemberFoldersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MemberFoldersComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MemberFoldersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
