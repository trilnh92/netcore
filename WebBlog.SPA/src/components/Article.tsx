import * as React from 'react';
import { BrowserRouter } from "react-router-dom"
import { Route } from "react-router-dom"
import { RouteComponentProps } from "react-router";
import { apiGetArticleById, apiGetArticles } from '../apiService';

interface IArticleState {
	article: any;
}

export class Article extends React.Component<RouteComponentProps<any>, IArticleState>{
	constructor(props: any) {
		super(props);

		this.state = {
			article: undefined
		}
	}

	loadArticle = (articleId: number) => {		
		apiGetArticleById(articleId, (response: any) => {
			if (response.target.status == 200) {
				let data = JSON.parse(response.target.responseText);
				this.setState({ article: data })
			}
		},
			(errors: any) => {
				this.setState({ article: undefined });
			})
	}

	componentDidMount() {
		const articleId = this.props.match.params.articleId ? this.props.match.params.articleId : 0;
		this.loadArticle(Number(articleId));
	}
	
	decodeHtml = (html:string) => {
		var txt = document.createElement("textarea");
		txt.innerHTML = html;
		return txt.value;
	}

	render() {
		return (
			<div>

				<h1 className="mt-4">{this.state.article?this.state.article.title:''}</h1>

				<p className="lead">
					<span>	by <a href="#">{this.state.article?this.state.article.createdBy:''}</a>
					</span>
				</p>
				<hr />


				<p>Posted on {this.state.article?this.state.article.createdDate:''}</p>

				<hr />


				<img className="img-fluid rounded" src="http://placehold.it/900x300" alt="" />

				<hr />
				{
					// this.decodeHtml(this.state.article?this.state.article.fullContent:'')				
				}
				<div dangerouslySetInnerHTML={{ __html: this.state.article?this.state.article.fullContent:''}} />
				<hr />


				<div className="card my-4">
					<h5 className="card-header">Leave a Comment:</h5>
					<div className="card-body">
						<form>
							<div className="form-group">
								<textarea className="form-control"></textarea>
							</div>
							<button type="submit" className="btn btn-primary">Submit</button>
						</form>
					</div>
				</div>


				<div className="media mb-4">
					<img className="d-flex mr-3 rounded-circle" src="http://placehold.it/50x50" alt="" />
					<div className="media-body">
						<h5 className="mt-0">Commenter Name</h5>
						Cras sit amet nibh libero, in gravida nulla. Nulla vel metus scelerisque ante sollicitudin. Cras purus odio, vestibulum in vulputate at, tempus viverra turpis. Fusce condimentum nunc ac nisi vulputate fringilla. Donec lacinia congue felis in faucibus.
                  </div>
				</div>


				<div className="media mb-4">
					<img className="d-flex mr-3 rounded-circle" src="http://placehold.it/50x50" alt="" />
					<div className="media-body">
						<h5 className="mt-0">Commenter Name</h5>
						Cras sit amet nibh libero, in gravida nulla. Nulla vel metus scelerisque ante sollicitudin. Cras purus odio, vestibulum in vulputate at, tempus viverra turpis. Fusce condimentum nunc ac nisi vulputate fringilla. Donec lacinia congue felis in faucibus.

                    <div className="media mt-4">
							<img className="d-flex mr-3 rounded-circle" src="http://placehold.it/50x50" alt="" />
							<div className="media-body">
								<h5 className="mt-0">Commenter Name</h5>
								Cras sit amet nibh libero, in gravida nulla. Nulla vel metus scelerisque ante sollicitudin. Cras purus odio, vestibulum in vulputate at, tempus viverra turpis. Fusce condimentum nunc ac nisi vulputate fringilla. Donec lacinia congue felis in faucibus.
                      </div>
						</div>

						<div className="media mt-4">
							<img className="d-flex mr-3 rounded-circle" src="http://placehold.it/50x50" alt="" />
							<div className="media-body">
								<h5 className="mt-0">Commenter Name</h5>
								Cras sit amet nibh libero, in gravida nulla. Nulla vel metus scelerisque ante sollicitudin. Cras purus odio, vestibulum in vulputate at, tempus viverra turpis. Fusce condimentum nunc ac nisi vulputate fringilla. Donec lacinia congue felis in faucibus.
                      </div>
						</div>

					</div>
				</div>

			</div>
		)
	}
}