<template>
<div class="todo-text" 
    title="Created on {{item.createDate}}">
    <!-- todo: use a conditional style -->
    <span v-if="item.isCompleted" style="text-decoration: line-trough;">{{item.description}}</span>
    <span v-else>{{item.description}}</span>
</div>
<div>
    <button @click="$emit('toggleComplete')"
        type="button" class="btn" :class="{ 'bg-orange' : item.isCompleted, 'bg-green': !item.isCompleted}" :title="toggleTitle">
        <i v-if="item.isCompleted" class="fas fa-times"></i>
        <i v-else class="fas fa-check"></i>
    </button>
    </div>
    <div>
        <button type="button" class="btn bg-blue" title="Detail"><i class="fas fa-sticky-note"></i></button>
    </div>
    <div>
        <button type="button" class="btn bg-blue" title="Edit"><i class="fas fa-pen"></i></button>
    </div>
    <div>
        <button @click="$emit('toggleArchived')" type="button" class="btn bg-red" title="Archive"><i class="fas fa-trash"></i></button>
    </div>
</template>

<script>

export default {
    props : {
        item: {
            type: Object,
            required: true
        }
    },
    emits: ["toggleComplete","toggleArchived"],
    data() {
        return {
        }
    },
    computed:{
        toggleTitle(){
            return this.item.isCompleted ? "undo completed" : "mark completed";
        }
    },
}
</script>