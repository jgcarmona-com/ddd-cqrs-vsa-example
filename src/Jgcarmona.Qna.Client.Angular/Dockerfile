FROM node:20 as build

WORKDIR /app
COPY package.json package-lock.json ./
RUN npm install
COPY . .
RUN npm run build --prod

FROM nginx:alpine
COPY --from=build /app/dist/qna-app /usr/share/nginx/html
EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
