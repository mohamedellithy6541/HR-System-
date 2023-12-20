import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GeneralSettingComponent } from './general-setting.component';

describe('GeneralSettingComponent', () => {
  let component: GeneralSettingComponent;
  let fixture: ComponentFixture<GeneralSettingComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GeneralSettingComponent]
    });
    fixture = TestBed.createComponent(GeneralSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
