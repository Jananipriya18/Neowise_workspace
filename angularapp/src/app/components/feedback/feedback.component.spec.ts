import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { FeedbackComponent } from './feedback.component';

describe('FeedbackComponent', () => {
  let component: FeedbackComponent;
  let fixture: ComponentFixture<FeedbackComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FeedbackComponent],
      imports: [FormsModule],
    });
    fixture = TestBed.createComponent(FeedbackComponent);
    component = fixture.componentInstance;
  });

  it('should create the feedback component', () => {
    expect((component as any)).toBeTruthy();
  });

  it('should initialize guestName rating and comment with default values', () => {
    expect((component as any).guestName).toEqual('');
    expect((component as any).rating).toEqual(5);
    expect((component as any).comment).toEqual('');
  });

  it('should add feedback to the feedbackList and clear the form after submission', () => {
    const feedbackData = {
      guestName: 'John Doe',
      rating: 4,
      comment: 'Great experience!',
    };

    (component as any).guestName = feedbackData.guestName;
    (component as any).rating = feedbackData.rating;
    (component as any).comment = feedbackData.comment;

    (component as any).submitFeedback();

    expect((component as any).feedbackList.length).toEqual(1);
    expect((component as any).feedbackList[0]).toEqual(feedbackData);
    expect((component as any).guestName).toEqual('');
    expect((component as any).rating).toEqual(1); // After submission, the rating should reset to 1
    expect((component as any).comment).toEqual('');
  });
  
});
