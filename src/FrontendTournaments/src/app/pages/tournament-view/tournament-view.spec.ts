import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TournamentView } from './tournament-view';

describe('TournamentView', () => {
  let component: TournamentView;
  let fixture: ComponentFixture<TournamentView>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TournamentView]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TournamentView);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
