import * as React from 'react';

interface ICreateBlogProps {
	userProfile:any;
}

interface ICreateBlogState {
	article:any;
	errorMessage:string;
	userProfile:any;
}

export class CreateBlog extends React.Component<ICreateBlogProps, ICreateBlogState>{
	constructor(props: any) {
		super(props);

		this.state = {
			article: undefined,
			errorMessage:'',
			userProfile:this.props.userProfile
		}
	}


	handleTextChange = (event: any, name: string) => {
        this.state.article[name] = event.target.value;
        this.setState({ article: this.state.article });
	}
	
	createBlog = (event:any) =>{
		event.preventDefault();

	}

	render() {
		return (
			<div>
			<h1 className="my-4">Write new blog
		</h1>
			<div className="panel-body" >
				<form className="form-horizontal" role="form">
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
									value={this.state.article ? this.state.article.title : ""}
									onChange={evt => this.handleTextChange(evt, 'title')}
									className="form-control" placeholder="Title" />
							</div>
						</div>
					</div>

					<div className="form-group">
						<div className="row">
							<label className="col-md-3 control-label">Brief content</label>
							<div className="col-md-9">
								<textarea rows={8}
									value={this.state.article? this.state.article.briefContent : ""}
									onChange={evt => this.handleTextChange(evt, 'briefContent')}
									className="form-control" placeholder="Brief Content"></textarea>
							</div>
						</div>
					</div>

					<div className="form-group">
						<div className="row">
							<label className="col-md-3 control-label">Image</label>
							<div className="col-md-9">
								<input type="file"
									value={this.state.article? this.state.article.image : ""}
									onChange={evt => this.handleTextChange(evt, 'image')}
									className="form-control" placeholder="Image" />
							</div>
						</div>
					</div>
					
					<div className="form-group">
						<div className="row">
							<label className="col-md-3 control-label">Full content</label>
							<div className="col-md-9">
								<textarea rows={30}
									value={this.state.article? this.state.article.fullContent : ""}
									onChange={evt => this.handleTextChange(evt, 'fullContent')}
									className="form-control" placeholder="Full Content"></textarea>
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