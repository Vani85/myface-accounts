﻿import React, { useContext } from 'react';
import {Page} from "../Page/Page";
import {fetchPosts, Post} from "../../Api/apiClient";
import {PostCard} from "../../Components/PostCard/PostCard";
import {InfiniteList} from "../../Components/InfititeList/InfiniteList";
import {Link} from "react-router-dom";
import './Feed.scss';
import { LoginContext } from '../../Components/LoginManager/LoginManager';

interface PostCardProps {
    post: Post;
}
export function Feed(): JSX.Element {
    const loginContext = useContext(LoginContext);
    return (
        <Page containerClassName={"feed"}>
            <h1 className="title">Feed</h1>
            <InfiniteList fetchItems={fetchPosts} renderItem={post => <PostCard key={post.id} post={post}/>}/>
            <Link className="create-post" to="/new-post">+</Link>
        </Page>
    );
}