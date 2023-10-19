import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BlogitemListComponent } from './blogitem-list.component';

describe('BlogitemListComponent', () => {
  let component: BlogitemListComponent;
  let fixture: ComponentFixture<BlogitemListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BlogitemListComponent]
    });
    fixture = TestBed.createComponent(BlogitemListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
