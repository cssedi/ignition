import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompleteChallengeComponent } from './complete-challenge.component';

describe('CompleteChallengeComponent', () => {
  let component: CompleteChallengeComponent;
  let fixture: ComponentFixture<CompleteChallengeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CompleteChallengeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CompleteChallengeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
