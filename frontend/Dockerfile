FROM node:lts-alpine as build
RUN mkdir /app
WORKDIR /app
COPY package.json /app
RUN npm install
COPY . /app
RUN npm run build

FROM nginx:alpine
COPY --from=build /app/nginx.config /etc/nginx/conf.d/default.conf
COPY --from=build /app/build /usr/share/nginx/html