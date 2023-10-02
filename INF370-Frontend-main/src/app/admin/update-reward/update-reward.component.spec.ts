import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateRewardComponent } from './update-reward.component';

describe('UpdateRewardComponent', () => {
  let component: UpdateRewardComponent;
  let fixture: ComponentFixture<UpdateRewardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateRewardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateRewardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
