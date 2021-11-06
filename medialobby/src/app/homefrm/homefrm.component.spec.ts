import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomefrmComponent } from './homefrm.component';

describe('HomefrmComponent', () => {
  let component: HomefrmComponent;
  let fixture: ComponentFixture<HomefrmComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HomefrmComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HomefrmComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
