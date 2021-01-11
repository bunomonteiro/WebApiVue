<template>
	<div class="about">
		<h1>This is the <u>Values</u> page</h1>
		<table>
			<thead>
				<tr v-if="fooList.length > 0">
					<th>#</th>
					<th>Name</th>
					<th>Active</th>
					<th>Actions</th>
				</tr>
				<tr v-else>
					<th></th>
				</tr>
			</thead>
			<tbody>
				<template v-if="fooList.length > 0">
					<tr v-for="foo in fooList" :key="foo.id">
						<td>{{ foo.id }}</td>
						<td>{{ foo.name }}</td>
						<td>{{ foo.isActive ? 'Yes' : 'No' }}</td>
						<td>
							<button v-if="foo.isActive" @click.prevent="deactivate(foo.id)">Deactivate</button>
							<button v-else @click.prevent="activate(foo.id)">Activate</button>
						</td>
					</tr>
				</template>
				<tr v-else>
					<td>No Records</td>
				</tr>
			</tbody>
		</table>
	</div>
</template>

<script lang="ts">
	import { defineComponent } from 'vue';

	export default defineComponent({
		name: 'about',
		data() {
			return {
				fooList: []
			}
		},
		beforeMount() {
			fetch('https://localhost:49161/api/foo/')
				.then(response => response.json())
				.then(data => {
					this.fooList = data;
				});
		},
		methods: {
			activate(id: number) {
				fetch(`https://localhost:49161/api/foo/${id}`)
					.then(response => response.json())
					.then(data => {
						alert(`Activate: ${data.name}`)
					});
			},
			deactivate(id: number) {
				fetch(`https://localhost:49161/api/foo/${id}`)
					.then(response => response.json())
					.then(data => {
						alert(`Activate: ${data.name}`)
					});
			}
		}
	});
</script>

<style scoped lang="scss">
	table {
		width: 100%;
	}
</style>