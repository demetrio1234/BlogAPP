import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './core/components/navbar/navbar.component';
import { CategoryListComponent } from './features/category/category-list/category-list.component';
import { AddCategoryComponent } from './features/category/add-category/add-category.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AddBlogitemComponent } from './features/blogitem/add-blogitem/add-blogitem.component';
import { BlogitemListComponent } from './features/blogitem/blogitem-list/blogitem-list.component';
import { AddUserComponent } from './features/user/add-user/add-user.component';
import { UserListComponent } from './features/user/user-list/user-list.component';
import { EditCategoryComponent } from './features/category/edit-category/edit-category.component'
import { MarkdownModule } from 'ngx-markdown';
import { EditBlogitemComponent } from './features/blogitem/edit-blogitem/edit-blogitem.component';
import { ImageuploaderComponent } from './shared/imageuploader/imageuploader.component';
import { ImageListComponent } from './shared/image-list/image-list.component';
import { BlogItemComponent } from './features/blogitem/blog-item/blog-item.component';
import { LoginComponent } from './features/auth/login/login.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    CategoryListComponent,
    AddCategoryComponent,
    AddBlogitemComponent,
    BlogitemListComponent,
    AddUserComponent,
    UserListComponent,
    EditCategoryComponent,
    EditBlogitemComponent,
    ImageuploaderComponent,
    ImageListComponent,
    BlogItemComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    MarkdownModule.forRoot(),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }