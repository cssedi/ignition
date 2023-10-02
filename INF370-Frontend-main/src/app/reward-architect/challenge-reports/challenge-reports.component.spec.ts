import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChallengeReportsComponent } from './challenge-reports.component';

describe('ChallengeReportsComponent', () => {
  let component: ChallengeReportsComponent;
  let fixture: ComponentFixture<ChallengeReportsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChallengeReportsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChallengeReportsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
