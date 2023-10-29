import { Category } from "../../category/models/category.model";

export interface AddBlogItemRequest {
    title: string;
    shortDescription: string;
    content: string;
    featuredImageUrl: string;
    urlHandle: string;
    publishedDate: Date;
    author: string;
    isVisible: boolean;
    categories: string[];
}  