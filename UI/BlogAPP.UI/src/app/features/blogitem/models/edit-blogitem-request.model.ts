export interface EditBlogItemRequest {
    title: string;
    shortdescription: string;
    content: string;
    featuredimageurl: string;
    urlhandle: string;
    publisheddate: Date;
    author: string;
    isvisible: string;
}