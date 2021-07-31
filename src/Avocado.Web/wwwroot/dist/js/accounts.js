/******/ (function(modules) { // webpackBootstrap
/******/ 	// install a JSONP callback for chunk loading
/******/ 	function webpackJsonpCallback(data) {
/******/ 		var chunkIds = data[0];
/******/ 		var moreModules = data[1];
/******/ 		var executeModules = data[2];
/******/
/******/ 		// add "moreModules" to the modules object,
/******/ 		// then flag all "chunkIds" as loaded and fire callback
/******/ 		var moduleId, chunkId, i = 0, resolves = [];
/******/ 		for(;i < chunkIds.length; i++) {
/******/ 			chunkId = chunkIds[i];
/******/ 			if(Object.prototype.hasOwnProperty.call(installedChunks, chunkId) && installedChunks[chunkId]) {
/******/ 				resolves.push(installedChunks[chunkId][0]);
/******/ 			}
/******/ 			installedChunks[chunkId] = 0;
/******/ 		}
/******/ 		for(moduleId in moreModules) {
/******/ 			if(Object.prototype.hasOwnProperty.call(moreModules, moduleId)) {
/******/ 				modules[moduleId] = moreModules[moduleId];
/******/ 			}
/******/ 		}
/******/ 		if(parentJsonpFunction) parentJsonpFunction(data);
/******/
/******/ 		while(resolves.length) {
/******/ 			resolves.shift()();
/******/ 		}
/******/
/******/ 		// add entry modules from loaded chunk to deferred list
/******/ 		deferredModules.push.apply(deferredModules, executeModules || []);
/******/
/******/ 		// run deferred modules when all chunks ready
/******/ 		return checkDeferredModules();
/******/ 	};
/******/ 	function checkDeferredModules() {
/******/ 		var result;
/******/ 		for(var i = 0; i < deferredModules.length; i++) {
/******/ 			var deferredModule = deferredModules[i];
/******/ 			var fulfilled = true;
/******/ 			for(var j = 1; j < deferredModule.length; j++) {
/******/ 				var depId = deferredModule[j];
/******/ 				if(installedChunks[depId] !== 0) fulfilled = false;
/******/ 			}
/******/ 			if(fulfilled) {
/******/ 				deferredModules.splice(i--, 1);
/******/ 				result = __webpack_require__(__webpack_require__.s = deferredModule[0]);
/******/ 			}
/******/ 		}
/******/
/******/ 		return result;
/******/ 	}
/******/
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// object to store loaded and loading chunks
/******/ 	// undefined = chunk not loaded, null = chunk preloaded/prefetched
/******/ 	// Promise = chunk loading, 0 = chunk loaded
/******/ 	var installedChunks = {
/******/ 		"accounts": 0
/******/ 	};
/******/
/******/ 	var deferredModules = [];
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId]) {
/******/ 			return installedModules[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			i: moduleId,
/******/ 			l: false,
/******/ 			exports: {}
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.l = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// define getter function for harmony exports
/******/ 	__webpack_require__.d = function(exports, name, getter) {
/******/ 		if(!__webpack_require__.o(exports, name)) {
/******/ 			Object.defineProperty(exports, name, { enumerable: true, get: getter });
/******/ 		}
/******/ 	};
/******/
/******/ 	// define __esModule on exports
/******/ 	__webpack_require__.r = function(exports) {
/******/ 		if(typeof Symbol !== 'undefined' && Symbol.toStringTag) {
/******/ 			Object.defineProperty(exports, Symbol.toStringTag, { value: 'Module' });
/******/ 		}
/******/ 		Object.defineProperty(exports, '__esModule', { value: true });
/******/ 	};
/******/
/******/ 	// create a fake namespace object
/******/ 	// mode & 1: value is a module id, require it
/******/ 	// mode & 2: merge all properties of value into the ns
/******/ 	// mode & 4: return value when already ns object
/******/ 	// mode & 8|1: behave like require
/******/ 	__webpack_require__.t = function(value, mode) {
/******/ 		if(mode & 1) value = __webpack_require__(value);
/******/ 		if(mode & 8) return value;
/******/ 		if((mode & 4) && typeof value === 'object' && value && value.__esModule) return value;
/******/ 		var ns = Object.create(null);
/******/ 		__webpack_require__.r(ns);
/******/ 		Object.defineProperty(ns, 'default', { enumerable: true, value: value });
/******/ 		if(mode & 2 && typeof value != 'string') for(var key in value) __webpack_require__.d(ns, key, function(key) { return value[key]; }.bind(null, key));
/******/ 		return ns;
/******/ 	};
/******/
/******/ 	// getDefaultExport function for compatibility with non-harmony modules
/******/ 	__webpack_require__.n = function(module) {
/******/ 		var getter = module && module.__esModule ?
/******/ 			function getDefault() { return module['default']; } :
/******/ 			function getModuleExports() { return module; };
/******/ 		__webpack_require__.d(getter, 'a', getter);
/******/ 		return getter;
/******/ 	};
/******/
/******/ 	// Object.prototype.hasOwnProperty.call
/******/ 	__webpack_require__.o = function(object, property) { return Object.prototype.hasOwnProperty.call(object, property); };
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "/";
/******/
/******/ 	var jsonpArray = window["webpackJsonp"] = window["webpackJsonp"] || [];
/******/ 	var oldJsonpFunction = jsonpArray.push.bind(jsonpArray);
/******/ 	jsonpArray.push = webpackJsonpCallback;
/******/ 	jsonpArray = jsonpArray.slice();
/******/ 	for(var i = 0; i < jsonpArray.length; i++) webpackJsonpCallback(jsonpArray[i]);
/******/ 	var parentJsonpFunction = oldJsonpFunction;
/******/
/******/
/******/ 	// add entry module to deferred list
/******/ 	deferredModules.push([0,"chunk-vendors","chunk-common"]);
/******/ 	// run deferred modules when ready
/******/ 	return checkDeferredModules();
/******/ })
/************************************************************************/
/******/ ({

/***/ "./src/crud-pages/accounts.ts":
/*!************************************!*\
  !*** ./src/crud-pages/accounts.ts ***!
  \************************************/
/*! no exports provided */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony import */ var C_Source_Avocado_src_Avocado_Web_node_modules_core_js_modules_es_array_iterator_js__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./node_modules/core-js/modules/es.array.iterator.js */ \"./node_modules/core-js/modules/es.array.iterator.js\");\n/* harmony import */ var C_Source_Avocado_src_Avocado_Web_node_modules_core_js_modules_es_array_iterator_js__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(C_Source_Avocado_src_Avocado_Web_node_modules_core_js_modules_es_array_iterator_js__WEBPACK_IMPORTED_MODULE_0__);\n/* harmony import */ var C_Source_Avocado_src_Avocado_Web_node_modules_core_js_modules_es_promise_js__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./node_modules/core-js/modules/es.promise.js */ \"./node_modules/core-js/modules/es.promise.js\");\n/* harmony import */ var C_Source_Avocado_src_Avocado_Web_node_modules_core_js_modules_es_promise_js__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(C_Source_Avocado_src_Avocado_Web_node_modules_core_js_modules_es_promise_js__WEBPACK_IMPORTED_MODULE_1__);\n/* harmony import */ var C_Source_Avocado_src_Avocado_Web_node_modules_core_js_modules_es_object_assign_js__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./node_modules/core-js/modules/es.object.assign.js */ \"./node_modules/core-js/modules/es.object.assign.js\");\n/* harmony import */ var C_Source_Avocado_src_Avocado_Web_node_modules_core_js_modules_es_object_assign_js__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(C_Source_Avocado_src_Avocado_Web_node_modules_core_js_modules_es_object_assign_js__WEBPACK_IMPORTED_MODULE_2__);\n/* harmony import */ var C_Source_Avocado_src_Avocado_Web_node_modules_core_js_modules_es_promise_finally_js__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./node_modules/core-js/modules/es.promise.finally.js */ \"./node_modules/core-js/modules/es.promise.finally.js\");\n/* harmony import */ var C_Source_Avocado_src_Avocado_Web_node_modules_core_js_modules_es_promise_finally_js__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(C_Source_Avocado_src_Avocado_Web_node_modules_core_js_modules_es_promise_finally_js__WEBPACK_IMPORTED_MODULE_3__);\n/* harmony import */ var _assureddt_pact_vue_grid__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @assureddt/pact-vue-grid */ \"./node_modules/@assureddt/pact-vue-grid/dist/pact-vue-grid.common.js\");\n/* harmony import */ var _assureddt_pact_vue_grid__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(_assureddt_pact_vue_grid__WEBPACK_IMPORTED_MODULE_4__);\n/* harmony import */ var _assureddt_pact_vue_grid_dist_pact_vue_grid_css__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @assureddt/pact-vue-grid/dist/pact-vue-grid.css */ \"./node_modules/@assureddt/pact-vue-grid/dist/pact-vue-grid.css\");\n/* harmony import */ var _assureddt_pact_vue_grid_dist_pact_vue_grid_css__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(_assureddt_pact_vue_grid_dist_pact_vue_grid_css__WEBPACK_IMPORTED_MODULE_5__);\n/* harmony import */ var _global__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ../global */ \"./src/global.ts\");\n\n\n\n\n\n\n\nnew _assureddt_pact_vue_grid__WEBPACK_IMPORTED_MODULE_4__[\"CRUDGrid\"](\"#accounts-crud\", {\n  gridOptions: {\n    read: \"/accounts/read\",\n    delete: \"/accounts/remove\",\n    pageSize: 10,\n    order: {\n      columnName: \"firstName\",\n      direction: _assureddt_pact_vue_grid__WEBPACK_IMPORTED_MODULE_4__[\"GridOrderDirection\"].ascending\n    },\n    allowEdit: true,\n    allowAdd: true,\n    allowDelete: true,\n    deleteColumn: \"firstName\",\n    buttonsWidth: 79\n  },\n  gridColumns: [{\n    name: \"firstName\",\n    display: \"First Name\",\n    type: \"text\"\n  }, {\n    name: \"lastName\",\n    display: \"Last Name\",\n    type: \"text\"\n  }],\n  editOptions: {\n    add: \"/accounts/add\",\n    edit: \"/accounts/edit\",\n    grabData: \"/accounts/data\",\n    editTitle: \"Edit Account\",\n    addTitle: \"Add Account\"\n  },\n  editFields: [{\n    name: \"firstName\",\n    display: \"First Name\",\n    placeholder: \"First Name\",\n    required: true,\n    type: \"text\"\n  }, {\n    name: \"lastName\",\n    display: \"Last Name\",\n    placeholder: \"Last Name\",\n    required: true,\n    type: \"text\"\n  }],\n  pageTitle: \"Accounts\"\n});//# sourceURL=[module]\n//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly8vLi9zcmMvY3J1ZC1wYWdlcy9hY2NvdW50cy50cz8zYzZjIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7Ozs7Ozs7Ozs7Ozs7Ozs7OztBQUFBO0FBQ0E7QUFDQTtBQUVBLElBQUksaUVBQUosQ0FBYSxnQkFBYixFQUErQjtBQUMzQixhQUFXLEVBQUU7QUFDVCxRQUFJLEVBQUUsZ0JBREc7QUFFVCxVQUFNLEVBQUUsa0JBRkM7QUFHVCxZQUFRLEVBQUUsRUFIRDtBQUlULFNBQUssRUFBRTtBQUNILGdCQUFVLEVBQUUsV0FEVDtBQUVILGVBQVMsRUFBRSwyRUFBa0IsQ0FBQztBQUYzQixLQUpFO0FBUVQsYUFBUyxFQUFFLElBUkY7QUFTVCxZQUFRLEVBQUUsSUFURDtBQVVULGVBQVcsRUFBRSxJQVZKO0FBV1QsZ0JBQVksRUFBRSxXQVhMO0FBWVQsZ0JBQVksRUFBRTtBQVpMLEdBRGM7QUFlM0IsYUFBVyxFQUFFLENBQ1Q7QUFDSSxRQUFJLEVBQUUsV0FEVjtBQUVJLFdBQU8sRUFBRSxZQUZiO0FBR0ksUUFBSSxFQUFFO0FBSFYsR0FEUyxFQU1UO0FBQ0ksUUFBSSxFQUFFLFVBRFY7QUFFSSxXQUFPLEVBQUUsV0FGYjtBQUdJLFFBQUksRUFBRTtBQUhWLEdBTlMsQ0FmYztBQTJCM0IsYUFBVyxFQUFFO0FBQ1QsT0FBRyxFQUFFLGVBREk7QUFFVCxRQUFJLEVBQUUsZ0JBRkc7QUFHVCxZQUFRLEVBQUUsZ0JBSEQ7QUFJVCxhQUFTLEVBQUUsY0FKRjtBQUtULFlBQVEsRUFBRTtBQUxELEdBM0JjO0FBa0MzQixZQUFVLEVBQUUsQ0FDUjtBQUNJLFFBQUksRUFBRSxXQURWO0FBRUksV0FBTyxFQUFFLFlBRmI7QUFHSSxlQUFXLEVBQUUsWUFIakI7QUFJSSxZQUFRLEVBQUUsSUFKZDtBQUtJLFFBQUksRUFBRTtBQUxWLEdBRFEsRUFRUjtBQUNJLFFBQUksRUFBRSxVQURWO0FBRUksV0FBTyxFQUFFLFdBRmI7QUFHSSxlQUFXLEVBQUUsV0FIakI7QUFJSSxZQUFRLEVBQUUsSUFKZDtBQUtJLFFBQUksRUFBRTtBQUxWLEdBUlEsQ0FsQ2U7QUFrRDNCLFdBQVMsRUFBRTtBQWxEZ0IsQ0FBL0IiLCJmaWxlIjoiLi9zcmMvY3J1ZC1wYWdlcy9hY2NvdW50cy50cy5qcyIsInNvdXJjZXNDb250ZW50IjpbImltcG9ydCB7IENSVURHcmlkLCBHcmlkT3JkZXJEaXJlY3Rpb24gfSBmcm9tIFwiQGFzc3VyZWRkdC9wYWN0LXZ1ZS1ncmlkXCI7XHJcbmltcG9ydCBcIkBhc3N1cmVkZHQvcGFjdC12dWUtZ3JpZC9kaXN0L3BhY3QtdnVlLWdyaWQuY3NzXCI7XHJcbmltcG9ydCBcIi4uL2dsb2JhbFwiO1xyXG5cclxubmV3IENSVURHcmlkKFwiI2FjY291bnRzLWNydWRcIiwge1xyXG4gICAgZ3JpZE9wdGlvbnM6IHtcclxuICAgICAgICByZWFkOiBcIi9hY2NvdW50cy9yZWFkXCIsXHJcbiAgICAgICAgZGVsZXRlOiBcIi9hY2NvdW50cy9yZW1vdmVcIixcclxuICAgICAgICBwYWdlU2l6ZTogMTAsXHJcbiAgICAgICAgb3JkZXI6IHtcclxuICAgICAgICAgICAgY29sdW1uTmFtZTogXCJmaXJzdE5hbWVcIixcclxuICAgICAgICAgICAgZGlyZWN0aW9uOiBHcmlkT3JkZXJEaXJlY3Rpb24uYXNjZW5kaW5nLFxyXG4gICAgICAgIH0sXHJcbiAgICAgICAgYWxsb3dFZGl0OiB0cnVlLFxyXG4gICAgICAgIGFsbG93QWRkOiB0cnVlLFxyXG4gICAgICAgIGFsbG93RGVsZXRlOiB0cnVlLFxyXG4gICAgICAgIGRlbGV0ZUNvbHVtbjogXCJmaXJzdE5hbWVcIixcclxuICAgICAgICBidXR0b25zV2lkdGg6IDc5LFxyXG4gICAgfSxcclxuICAgIGdyaWRDb2x1bW5zOiBbXHJcbiAgICAgICAge1xyXG4gICAgICAgICAgICBuYW1lOiBcImZpcnN0TmFtZVwiLFxyXG4gICAgICAgICAgICBkaXNwbGF5OiBcIkZpcnN0IE5hbWVcIixcclxuICAgICAgICAgICAgdHlwZTogXCJ0ZXh0XCIsXHJcbiAgICAgICAgfSxcclxuICAgICAgICB7XHJcbiAgICAgICAgICAgIG5hbWU6IFwibGFzdE5hbWVcIixcclxuICAgICAgICAgICAgZGlzcGxheTogXCJMYXN0IE5hbWVcIixcclxuICAgICAgICAgICAgdHlwZTogXCJ0ZXh0XCIsXHJcbiAgICAgICAgfSxcclxuICAgIF0sXHJcbiAgICBlZGl0T3B0aW9uczoge1xyXG4gICAgICAgIGFkZDogXCIvYWNjb3VudHMvYWRkXCIsXHJcbiAgICAgICAgZWRpdDogXCIvYWNjb3VudHMvZWRpdFwiLFxyXG4gICAgICAgIGdyYWJEYXRhOiBcIi9hY2NvdW50cy9kYXRhXCIsXHJcbiAgICAgICAgZWRpdFRpdGxlOiBcIkVkaXQgQWNjb3VudFwiLFxyXG4gICAgICAgIGFkZFRpdGxlOiBcIkFkZCBBY2NvdW50XCIsXHJcbiAgICB9LFxyXG4gICAgZWRpdEZpZWxkczogW1xyXG4gICAgICAgIHtcclxuICAgICAgICAgICAgbmFtZTogXCJmaXJzdE5hbWVcIixcclxuICAgICAgICAgICAgZGlzcGxheTogXCJGaXJzdCBOYW1lXCIsXHJcbiAgICAgICAgICAgIHBsYWNlaG9sZGVyOiBcIkZpcnN0IE5hbWVcIixcclxuICAgICAgICAgICAgcmVxdWlyZWQ6IHRydWUsXHJcbiAgICAgICAgICAgIHR5cGU6IFwidGV4dFwiLFxyXG4gICAgICAgIH0sXHJcbiAgICAgICAge1xyXG4gICAgICAgICAgICBuYW1lOiBcImxhc3ROYW1lXCIsXHJcbiAgICAgICAgICAgIGRpc3BsYXk6IFwiTGFzdCBOYW1lXCIsXHJcbiAgICAgICAgICAgIHBsYWNlaG9sZGVyOiBcIkxhc3QgTmFtZVwiLFxyXG4gICAgICAgICAgICByZXF1aXJlZDogdHJ1ZSxcclxuICAgICAgICAgICAgdHlwZTogXCJ0ZXh0XCIsXHJcbiAgICAgICAgfVxyXG4gICAgXSxcclxuICAgIHBhZ2VUaXRsZTogXCJBY2NvdW50c1wiLFxyXG59KTsiXSwic291cmNlUm9vdCI6IiJ9\n//# sourceURL=webpack-internal:///./src/crud-pages/accounts.ts\n");

/***/ }),

/***/ 0:
/*!******************************************!*\
  !*** multi ./src/crud-pages/accounts.ts ***!
  \******************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! C:\Source\Avocado\src\Avocado.Web\src\crud-pages\accounts.ts */"./src/crud-pages/accounts.ts");


/***/ })

/******/ });