var options = {
	stripBanners: true,
	banner: '/**\n' +
	' * Application:  <%= pkg.name %>\n' +
	' * Version:      <%= pkg.version %>\n' +
	' * Release date: <%= grunt.template.today("yyyy-mm-dd") %>\n' +
	' * Author:       <%= pkg.author %> (<%= pkg.authorPage %>)\n' +
	' * Homepage:     https://github.com/StepanMas/jQuery.miniNoty\n' +
	' * License:      <%= pkg.license %>\n' +
	'*/\n\n'
}

module.exports = function (grunt) {

	grunt.initConfig({
		pkg: grunt.file.readJSON('package.json'),

		uglify: {
			options: options,
			build: {
				src: '../<%= pkg.name %>.js',
				dest: '../<%= pkg.name %>.min.js'
			}
		},
		concat: {
			options: options,
			dist: {
				src: [
					'../<%= pkg.name %>.dev.js'
				],
				dest: '../<%= pkg.name %>.js'
			}
		},
		watch: {
			scripts: {
				files: ['../<%= pkg.name %>.dev.js'],
				tasks: ['concat', 'uglify'],
				options: {
					spawn: false
				}
			}
		}

	});

	// 3. Тут мы указываем Grunt, что хотим использовать этот плагин
	grunt.loadNpmTasks('grunt-contrib-concat');
	grunt.loadNpmTasks('grunt-contrib-watch');
	grunt.loadNpmTasks('grunt-contrib-uglify');

	// 4. Указываем, какие задачи выполняются, когда мы вводим «grunt» в терминале
	grunt.registerTask('default', ['concat', 'uglify', 'watch']);

};