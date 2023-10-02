import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RewardArchitectDashboardComponent } from './reward-architect-dashboard.component';

describe('RewardArchitectDashboardComponent', () => {
  let component: RewardArchitectDashboardComponent;
  let fixture: ComponentFixture<RewardArchitectDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RewardArchitectDashboardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RewardArchitectDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
