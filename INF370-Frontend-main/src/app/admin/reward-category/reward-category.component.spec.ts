import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RewardCategoryComponent } from './reward-category.component';

describe('RewardCategoryComponent', () => {
  let component: RewardCategoryComponent;
  let fixture: ComponentFixture<RewardCategoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RewardCategoryComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RewardCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
