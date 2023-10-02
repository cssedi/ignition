import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuizCatergoryComponent } from './quiz-catergory.component';

describe('QuizCatergoryComponent', () => {
  let component: QuizCatergoryComponent;
  let fixture: ComponentFixture<QuizCatergoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ QuizCatergoryComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(QuizCatergoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
