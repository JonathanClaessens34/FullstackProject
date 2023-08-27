FROM node:14.20-alpine AS build

WORKDIR /usr/src/app

COPY . .

RUN npm install
RUN npm run build

FROM nginx:alpine as prod-build

COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=build /usr/src/app/dist/frontend-web /usr/share/nginx/html

ARG API_URL

RUN envsubst '$API_URL' < /etc/nginx/nginx.conf > temp.conf && mv temp.conf /etc/nginx/nginx.conf
