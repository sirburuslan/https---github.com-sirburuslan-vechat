export default interface ElementsResponse<T> {
    result: {
        elements: T;
        page: number;
        total: number;
    };
    success: boolean;
    message: string;
    time: number;
}
