import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewRewardShopComponent } from './view-reward-shop.component';

describe('ViewRewardShopComponent', () => {
  let component: ViewRewardShopComponent;
  let fixture: ComponentFixture<ViewRewardShopComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewRewardShopComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewRewardShopComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
