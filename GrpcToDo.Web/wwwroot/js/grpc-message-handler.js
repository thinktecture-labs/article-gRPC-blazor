const postType = "__GRPCWEB_DEVTOOLS__";

export function sendGrpcRequest(method, methodType, request, response) {
    if (response && request) {
        window.postMessage({
            type: postType,
            method,
            methodType,
            request,
            response
        });
    } else if (request) {
        window.postMessage({
            type: postType,
            method,
            methodType,
            request
        });
    } else if (response) {
        window.postMessage({
            type: postType,
            method,
            methodType,
            response
        });
    }
}