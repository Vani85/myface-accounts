import React, {useContext, useEffect, useState} from 'react';
import {ListResponse, Post} from "../../Api/apiClient";
import {Grid} from "../Grid/Grid";
import {PostCard} from "../PostCard/PostCard";
import { LoginContext } from '../LoginManager/LoginManager';

interface PostListProps {
    title: string,
    fetchPosts: () => Promise<ListResponse<Post>>
}

export function PostList(props: PostListProps): JSX.Element {
    const [posts, setPosts] = useState<Post[]>([]);
    const loginContext = useContext(LoginContext);
    
    useEffect(() => {
        props.fetchPosts()
            .then(response => setPosts(response.items));
    }, [props]);
    
    return (
        <section>
            <h2>{props.title}</h2>
            <Grid>
                {posts.map(post => <PostCard key={post.id} post={post}/>)}
            </Grid>
        </section>
    );
}