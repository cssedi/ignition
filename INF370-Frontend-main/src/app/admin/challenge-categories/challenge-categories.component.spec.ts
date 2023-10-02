import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChallengeCategoriesComponent } from './challenge-categories.component';

describe('ChallengeCategoriesComponent', () => {
  let component: ChallengeCategoriesComponent;
  let fixture: ComponentFixture<ChallengeCategoriesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChallengeCategoriesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChallengeCategoriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
