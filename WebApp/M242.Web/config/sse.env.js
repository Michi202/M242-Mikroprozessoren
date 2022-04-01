'use strict'
const { merge } = require('webpack-merge');
const DevENV = require('./prod.env')

module.exports = merge(DevENV, {
  NODE_ENV: 'developments',
  API_URL: 'http://localhost/M242/api/',
})
