import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { FlightFeedbackComponent } from './flight-feedback.component';

describe('FlightFeedbackComponent', () => {
  let component: FlightFeedbackComponent;
  let fixture: ComponentFixture<FlightFeedbackComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FlightFeedbackComponent],
      imports: [FormsModule],
    });
    fixture = TestBed.createComponent(FlightFeedbackComponent);
    component = fixture.componentInstance;
  });

  it('should create the feedback component', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize passengerName, rating, and comment with default values', () => {
    expect(component.passengerName).toEqual('');
    expect(component.rating).toEqual(5);
    expect(component.comment).toEqual('');
  });

  it('should add feedback to the feedbackList and clear the form after submission', () => {
    const feedbackData = {
      passengerName: 'Jane Doe',
      rating: 4,
      comment: 'Excellent flight!',
    };

    component.passengerName = feedbackData.passengerName;
    component.rating = feedbackData.rating;
    component.comment = feedbackData.comment;

    component.submitFeedback();

    expect(component.feedbackList.length).toEqual(1);
    expect(component.feedbackList[0]).toEqual(feedbackData);
    expect(component.passengerName).toEqual('');
    expect(component.rating).toEqual(1); // After submission, the rating should reset to 1
    expect(component.comment).toEqual('');
  });
});
