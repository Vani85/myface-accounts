let base64 = require('base-64');
export interface ListResponse<T> {
    items: T[];
    totalNumberOfItems: number;
    page: number;
    nextPage: string;
    previousPage: string;
}

export interface User {
    id: number;
    firstName: string;
    lastName: string;
    displayName: string;
    username: string;
    email: string;
    profileImageUrl: string;
    coverImageUrl: string;
}

export interface Interaction {
    id: number;
    user: User;
    type: string;
    date: string;
}

export interface Post {
    id: number;
    message: string;
    imageUrl: string;
    postedAt: string;
    postedBy: User;
    likes: Interaction[];
    dislikes: Interaction[];
}

export interface NewPost {
    message: string;
    imageUrl: string;
    userId: number;
}

export async function fetchAPI(url:string,username:string,password:string) {
    const credentials = btoa(`${username}:${password}`);
    const response = await fetch(`https://localhost:5001/${url}`,
        {
            method: 'GET', 
            headers: {
              "Authorization": `Basic ${credentials}`,
              "Content-Type": 'application/json',
              "Access-Control-Allow-Origin": "*"
            }
        });
    return await response.json();
}
export async function fetchUsers(username:string, password:string,searchTerm: string, page: number, pageSize: number): Promise<ListResponse<User>> {   
    return await fetchAPI(`users?search=${searchTerm}&page=${page}&pageSize=${pageSize}`,username,password);
}

export async function fetchUser(username:string, password:string,userId: string | number): Promise<User> {
    return await fetchAPI(`users/${userId}`,username,password);
}

export async function fetchPosts(username:string, password:string,page: number, pageSize: number): Promise<ListResponse<Post>> {
    return await fetchAPI(`feed?page=${page}&pageSize=${pageSize}`,username,password);
}

export async function fetchPostsForUser(username:string, password:string,page: number, pageSize: number, userId: string | number) {
    return await fetchAPI(`feed?page=${page}&pageSize=${pageSize}&postedBy=${userId}`,username,password);
}

export async function fetchPostsLikedBy(username:string, password:string,page: number, pageSize: number, userId: string | number) {
    return await fetchAPI(`feed?page=${page}&pageSize=${pageSize}&likedBy=${userId}`,username,password);
}

export async function fetchPostsDislikedBy(username:string, password:string,page: number, pageSize: number, userId: string | number) {
    return await fetchAPI(`feed?page=${page}&pageSize=${pageSize}&dislikedBy=${userId}`,username,password);
}

export async function createPost(username:string, password:string,newPost: NewPost) {
    const credentials = btoa(`${username}:${password}`);
    const response = await fetch(`https://localhost:5001/posts/create`, {
        method: "POST",
        headers: {
            "Authorization": `Basic ${credentials}`,
            "Content-Type": "application/json"
        },
        body: JSON.stringify(newPost),
    });
    
    if (!response.ok) {
        throw new Error(await response.json())
    }
}
