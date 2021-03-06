export class ArticleModel {
    ArticleId: number = -1;
    CategoryArticleId: any;
    CategoryArticleName: string= '';
    ViewCount: number= -1;
    Image: string= '';
    Title: string= '';
    BriefContent: string= '';
    FullContent: string= '';
    Source: string= '';
    Index: any;
    IsVisible: boolean= false;
    SEName: string= '';
    SEOTitle: string= '';
    SEODescription: string= '';
    SEOKeywords: string= '';
    IsDeleted: boolean= false;
    IsHot: boolean = false;
    DeletedDate: any;
    DeletedBy: any;
    UpdatedDate: any;
    UpdatedBy: string= '';
    CreatedBy: string= '';
    CreatedDate: any;
    Position: any;
    Ext: string = '';
    Ext1: string= '';
    Ext2: string= '';
    Ext3: string= '';
}

export class CreateArticleViewModel{
    Title:string = '';
    CreatedBy:string = '';
    Tags:any;
    Image:string='';
    FullContent:string = '';
}