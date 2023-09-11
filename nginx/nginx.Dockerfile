FROM nginx

# NOTE!!!
# Even though we keep nginx.local.conf inside /nginx solution folder,
# docker tool is not aware of it since it is only a Visual Studio construct.
COPY nginx/nginx.local.conf /etc/nginx/nginx.conf
#COPY nginx/api-local.testtemplate4.crt /etc/ssl/certs/api-local.testtemplate4.com.crt
#COPY nginx/api-local.testtemplate4.key /etc/ssl/private/api-local.testtemplate4.com.key