FROM node:16.20.0-alpine

# set working directory
WORKDIR /app

# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH

# install and cache app dependencies
COPY package*.json ./

CMD ["node", "-v"]

RUN npm install

COPY . .

# start app
CMD ["npm", "run", "serve"]