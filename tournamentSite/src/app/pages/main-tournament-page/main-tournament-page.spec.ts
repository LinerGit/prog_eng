import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MainTournamentPage } from './main-tournament-page';

describe('MainTournamentPage', () => {
  let component: MainTournamentPage;
  let fixture: ComponentFixture<MainTournamentPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MainTournamentPage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MainTournamentPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
