import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RewardArchitectsComponent } from './reward-architects.component';

describe('RewardArchitectsComponent', () => {
  let component: RewardArchitectsComponent;
  let fixture: ComponentFixture<RewardArchitectsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RewardArchitectsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RewardArchitectsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
