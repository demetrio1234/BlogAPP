import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditBlogitemComponent } from './edit-blogitem.component';

describe('EditBlogitemComponent', () => {
  let component: EditBlogitemComponent;
  let fixture: ComponentFixture<EditBlogitemComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditBlogitemComponent]
    });
    fixture = TestBed.createComponent(EditBlogitemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
