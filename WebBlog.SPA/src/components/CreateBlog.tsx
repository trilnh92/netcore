import * as React from 'react';
import { apiCreateArticle, apiUploadPhoto } from './../apiService'
import { Redirect } from 'react-router-dom'
import { CreateArticleViewModel } from './../models/article.model'

interface ICreateBlogProps {
	userProfile: any;
}

interface ICreateBlogState {
	article: CreateArticleViewModel;
	errorMessage: string;
	userProfile: any;
	redirectToMyBlogs: boolean;
	file:any;
}

export class CreateBlog extends React.Component<ICreateBlogProps, ICreateBlogState>{
	constructor(props: any) {
		super(props);

		this.state = {
			article: new CreateArticleViewModel(),
			errorMessage: '',
			userProfile: this.props.userProfile,
			redirectToMyBlogs: false,
			file:undefined
		}
	}

	handleImageChange = (event: any) => {
		event.preventDefault();
		let reader = new FileReader();
		let file = event.target.files[0];

		reader.onloadend = () => {
			this.setState({file: file});
			this.uploadPhoto();
		}
		reader.readAsDataURL(file)
	}

	
    uploadPhoto() {
        apiUploadPhoto(this.state.file, (response:any) => {
            if (response.target.status == 200) {
                let data = JSON.parse(response.target.responseText);
                if (data.success) {
                    this.state.article.Image = data.data;
                    this.setState({ article: this.state.article });
				}
				else
				{
					this.setState({ redirectToMyBlogs: false, errorMessage: data.errorMessage });
				}
            }
        },
            (errors:any) => {
				this.setState({ redirectToMyBlogs: false, errorMessage: 'Error when upload image' });
            })
    }

	handleTextChange = (event: any, name: string) => {

		switch (name) {
			case 'title':
				this.state.article.Title = event.target.value;
				break;
			case 'fullcontent':
				this.state.article.FullContent = event.target.value;
				break;
			case 'tags':
				this.state.article.Tags = event.target.value;
				break;
		}
		this.setState({ article: this.state.article });
	}

	createBlog = (event: any) => {
		event.preventDefault();
		this.setState({ errorMessage: '' });

		var article = this.state.article;
		article.CreatedBy = this.state.userProfile ? this.state.userProfile.email : '';

		apiCreateArticle(article, (response: any) => {
			if (response.target.status == 200) {
				let data = JSON.parse(response.target.responseText);
				if (data.success) {
					this.setState({ redirectToMyBlogs: true });
				}
				else {
					this.setState({ redirectToMyBlogs: false, errorMessage: data.errorMessage });
				}
			}
			else {
				this.setState({ redirectToMyBlogs: false, errorMessage: "Error when write new blog from server" });
			}
		},
			(errors: any) => {
				this.setState({ redirectToMyBlogs: false, errorMessage: 'Error when write new blog account!' });
			})

	}

	render() {
		
		if(this.props.userProfile == undefined || this.props.userProfile.email == undefined){
            return <Redirect to='/' />;
		}
		
		if (this.state.redirectToMyBlogs) {
			return <Redirect to='/myblogs' />;
		}

		return (
			<div>
				<h1 className="my-4">Write new blog
		</h1>
				<div className="panel-body" >
					<form className="form-horizontal" role="form" encType="multipart/form-data">
						{
							(() => {
								if (this.state.errorMessage != '') {
									return (
										<div className="alert alert-danger">
											<p>{this.state.errorMessage}</p>
											<span></span>
										</div>
									)
								}
							})()
						}

						<div className="form-group">
							<div className="row">
								<label className="col-md-3 control-label">Title</label>
								<div className="col-md-9">
									<input type="text"
										value={this.state.article ? this.state.article.Title : ""}
										onChange={evt => this.handleTextChange(evt, 'title')}
										className="form-control" placeholder="Title" />
								</div>
							</div>
						</div>

						<div className="form-group">
							<div className="row">
								<label className="col-md-3 control-label">Image</label>
								<div className="col-md-6">
									<input type="file" accept="image/*"
										onChange={this.handleImageChange}
										className="form-control" placeholder="Image" />

								</div>
								<div className="col-md-3">
									{this.state.article.Image ?
                                        (
                                            <img width="100%" src={this.state.article.Image} alt="icon" />
                                        ) : (
                                            <img width="100%" src="images/img_not_available.png" alt="icon" />
                                        )
                                    }
								</div>

							</div>
						</div>

						<div className="form-group">
							<div className="row">
								<label className="col-md-3 control-label">Full content</label>
								<div className="col-md-9">
									<textarea rows={20}
										value={this.state.article ? this.state.article.FullContent : ""}
										onChange={evt => this.handleTextChange(evt, 'fullcontent')}
										className="form-control" placeholder="Full Content"></textarea>
								</div>
							</div>
						</div>

						<div className="form-group">
							<div className="row">
								<label className="col-md-3 control-label">Tags</label>
								<div className="col-md-9">
									<input type="text"
										value={this.state.article ? this.state.article.Tags : ""}
										onChange={evt => this.handleTextChange(evt, 'tags')}
										className="form-control" placeholder="Tags" />
								</div>
							</div>
						</div>

						<div className="form-group">
							<div className="col-md-offset-3 col-md-9">
								<button className="btn btn-info" type="button"
									onClick={(e) => this.createBlog(e)}
								><i className="icon-hand-right"></i> Write new blog</button>
							</div>
						</div>
					</form>
				</div>
			</div>
		)
	}
}