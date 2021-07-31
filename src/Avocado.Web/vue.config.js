const path = require("path");

module.exports = {
	filenameHashing: false,
	productionSourceMap: false,
	outputDir: path.resolve(__dirname, "./wwwroot/build"),
	assetsDir: "../dist",
	pages: {
        accounts: {
			entry: "src/crud-pages/accounts.ts",
		},
        empty: {
            entry: "src/global.ts",
        }
    },
	configureWebpack: {
		devtool: process.env.NODE_ENV !== "production" ? "eval-source-map" : false,
		resolve: {
			symlinks: false,
			alias: {
				vue: path.resolve("./node_modules/vue"),
			},
		},
	},
};
