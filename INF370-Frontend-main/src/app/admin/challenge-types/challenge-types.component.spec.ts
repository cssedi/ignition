import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChallengeTypesComponent } from './challenge-types.component';

describe('ChallengeTypesComponent', () => {
  let component: ChallengeTypesComponent;
  let fixture: ComponentFixture<ChallengeTypesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChallengeTypesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChallengeTypesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
