import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RewardShopComponent } from './reward-shop.component';

describe('RewardShopComponent', () => {
  let component: RewardShopComponent;
  let fixture: ComponentFixture<RewardShopComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RewardShopComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RewardShopComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
