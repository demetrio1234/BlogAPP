import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddBlogitemComponent } from './add-blogitem.component';

describe('AddBlogitemComponent', () => {
  let component: AddBlogitemComponent;
  let fixture: ComponentFixture<AddBlogitemComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddBlogitemComponent]
    });
    fixture = TestBed.createComponent(AddBlogitemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
